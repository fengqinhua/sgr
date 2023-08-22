/**************************************************************
 * 
 * 唯一标识：68163bf3-79a4-4a74-baa6-51e92484b775
 * 命名空间：Sgr.Generator
 * 创建时间：2023/7/28 16:31:37
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/


using Sgr.Exceptions;
using Sgr.Licence;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Sgr.Generator
{
    /**
         * 
         * 算法描述：
         * snowflake算法 64 位
         * 0---0000000000 0000000000 0000000000 0000000000 0 --- 00000 ---00000 ---000000000000
         * 1bit，不用，因为二进制中最高位是符号位，1表示负数，0表示正数。生成的id一般都是用整数，所以最高位固定为0。
         * 41位的时间序列，精确到毫秒级，41位的长度可以使用69年。时间位还有一个很重要的作用是可以根据时间进行排序。
         * 10位的机器标识，10位的长度最多支持部署1024个节点，包括5位datacenterId和5位workerId
         *     5位（bit）可以表示的最大正整数是2^{5}-1 = 31，即可以用0、1、2、3、…31这32个数字，来表示不同的datecenterId或workerId
         * 12位的计数序列号，序列号即一系列的自增id，可以支持同一节点同一毫秒生成多个ID序号，12位的计数序列号支持每个节点每毫秒产生4096个ID序号。
         */

    /// <summary>
    /// 分布式ID算法（雪花算法）
    /// </summary>
    internal class Snowflake
    {
        private readonly long _machineId;//机器ID
        private readonly long _datacenterId = 0L;//数据ID

        private static readonly long twepoch = 687888001020L; //唯一时间随机量

        private static readonly long machineIdBits = 5L; //机器码字节数
        private static readonly long datacenterIdBits = 5L;//数据字节数
        private static readonly long maxMachineId = -1L ^ -1L << (int)machineIdBits; //最大机器ID
        private static readonly long maxDatacenterId = -1L ^ -1L << (int)datacenterIdBits;//最大数据ID

        private static readonly long sequenceBits = 12L; //计数器字节数，12个字节用来保存计数码        
        private static readonly long machineIdShift = sequenceBits; //机器码数据左移位数，就是后面计数器占用的位数
        private static readonly long datacenterIdShift = sequenceBits + machineIdBits;
        private static readonly long timestampLeftShift = sequenceBits + machineIdBits + datacenterIdBits; //时间戳左移动位数就是机器码+计数器总字节数+数据字节数
        private static readonly long sequenceMask = -1L ^ -1L << (int)sequenceBits; //一微秒内可以产生计数，如果达到该值则等到下一微妙在进行生成
        
        private long lastTimestamp = -1L;//最后时间戳
        private long _sequence = 0L;//计数从零开始

        private static readonly object syncRoot = new object();//加锁对象

        public Snowflake() : this(0L, 0L) { }
        public Snowflake(long machineId):this(machineId, 0L) { }

        public Snowflake(long machineId, long datacenterId)
        {
            if (machineId >= 0)
            {
                if (machineId > maxMachineId)
                    throw new BusinessException("机器码ID非法");

                this._machineId = machineId;
            }
            if (datacenterId >= 0)
            {
                if (datacenterId > maxDatacenterId)
                    throw new BusinessException("数据中心ID非法");

                this._datacenterId = datacenterId;
            }
        }

        /// <summary>
        /// 生成当前时间戳
        /// </summary>
        /// <returns>毫秒</returns>
        private static long GetTimestamp()
        {
            return (long)(DateTime.UtcNow - new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }

        /// <summary>
        /// 获取下一微秒时间戳
        /// </summary>
        /// <param name="lastTimestamp"></param>
        /// <returns></returns>
        private static long GetNextTimestamp(long lastTimestamp)
        {
            long timestamp = GetTimestamp();
            if (timestamp <= lastTimestamp)
            {
                timestamp = GetTimestamp();
            }
            return timestamp;
        }

        /// <summary>
        /// 获取长整型的ID
        /// </summary>
        /// <returns></returns>
        public long GetId()
        {
            lock (syncRoot)
            {
                long timestamp = GetTimestamp();
                if (lastTimestamp == timestamp)
                { //同一微妙中生成ID
                    this._sequence = this._sequence + 1 & sequenceMask; //用&运算计算该微秒内产生的计数是否已经到达上限
                    if (this._sequence == 0)
                    {
                        //一微妙内产生的ID计数已达上限，等待下一微妙
                        timestamp = GetNextTimestamp(lastTimestamp);
                    }
                }
                else
                {
                    //不同微秒生成ID
                    this._sequence = 0L;
                }
                if (timestamp < lastTimestamp)
                {
                    throw new BusinessException("时间戳比上一次生成ID时时间戳还小，故异常");
                }
                lastTimestamp = timestamp; //把当前时间戳保存为最后生成ID的时间戳
                long Id = timestamp - twepoch << (int)timestampLeftShift
                    | this._datacenterId << (int)datacenterIdShift
                    | this._machineId << (int)machineIdShift
                    | this._sequence;
                return Id;
            }
        }
    }



    ///// <summary>
    ///// 根据twitter的snowflake算法生成唯一ID
    ///// snowflake算法 64 位
    ///// 0---0000000000 0000000000 0000000000 0000000000 0 --- 00000 ---00000 ---000000000000
    ///// 第一位为未使用（实际上也可作为long的符号位），接下来的41位为毫秒级时间，然后5位datacenter标识位，5位机器ID（并不算标识符，实际是为线程标识），然后12位该毫秒内的当前毫秒内的计数，加起来刚好64位，为一个Long型。
    ///// 其中datacenter标识位起始是机器位，机器ID其实是线程标识，可以同一一个10位来表示不同机器
    ///// </summary>
    //public class SnowflakeIdWorker
    //{
    //    //机器ID
    //    private static long workerId;
    //    private static long twepoch = 687888001020L; //唯一时间，这是一个避免重复的随机量，自行设定不要大于当前时间戳
    //    private static long sequence = 0L;
    //    private static int workerIdBits = 4; //机器码字节数。4个字节用来保存机器码
    //    public static long maxWorkerId = -1L ^ -1L << workerIdBits; //最大机器ID
    //    private static int sequenceBits = 10; //计数器字节数，10个字节用来保存计数码
    //    private static int workerIdShift = sequenceBits; //机器码数据左移位数，就是后面计数器占用的位数
    //    private static int timestampLeftShift = sequenceBits + workerIdBits; //时间戳左移动位数就是机器码和计数器总字节数
    //    public static long sequenceMask = -1L ^ -1L << sequenceBits; //一微秒内可以产生计数，如果达到该值则等到下一微妙在进行生成
    //    private long lastTimestamp = -1L;

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="workerId"> 5位（bit）可以表示的最大正整数是2^5−1=31，即可以用0、1、2、3、....31这32个数字，来表示不同的workerId</param>
    //    public SnowflakeIdWorker(long workerId)
    //    {
    //        if (workerId > maxWorkerId || workerId < 0)
    //            throw new Exception(string.Format("worker Id can't be greater than {0} or less than 0 ", workerId));
    //        SnowflakeIdWorker.workerId = workerId;
    //    }

    //    public long NextId()
    //    {
    //        lock (this)
    //        {
    //            long timestamp = timeGen();
    //            if (this.lastTimestamp == timestamp)
    //            { //同一微妙中生成ID
    //                SnowflakeIdWorker.sequence = (SnowflakeIdWorker.sequence + 1) & SnowflakeIdWorker.sequenceMask; //用&运算计算该微秒内产生的计数是否已经到达上限
    //                if (SnowflakeIdWorker.sequence == 0)
    //                {
    //                    //一微妙内产生的ID计数已达上限，等待下一微妙
    //                    timestamp = tillNextMillis(this.lastTimestamp);
    //                }
    //            }
    //            else
    //            { //不同微秒生成ID
    //                SnowflakeIdWorker.sequence = 0; //计数清0
    //            }
    //            if (timestamp < lastTimestamp)
    //            { //如果当前时间戳比上一次生成ID时时间戳还小，抛出异常，因为不能保证现在生成的ID之前没有生成过
    //                throw new Exception(string.Format("Clock moved backwards.  Refusing to generate id for {0} milliseconds",
    //                    this.lastTimestamp - timestamp));
    //            }
    //            this.lastTimestamp = timestamp; //把当前时间戳保存为最后生成ID的时间戳
    //            long nextId = (timestamp - twepoch << timestampLeftShift) | SnowflakeIdWorker.workerId << SnowflakeIdWorker.workerIdShift | SnowflakeIdWorker.sequence;
    //            return nextId;
    //        }
    //    }

    //    /// <summary>
    //    /// 获取下一微秒时间戳
    //    /// </summary>
    //    /// <param name="lastTimestamp"></param>
    //    /// <returns></returns>
    //    private long tillNextMillis(long lastTimestamp)
    //    {
    //        long timestamp = timeGen();
    //        while (timestamp <= lastTimestamp)
    //        {
    //            timestamp = timeGen();
    //        }
    //        return timestamp;
    //    }

    //    /// <summary>
    //    /// 生成当前时间戳
    //    /// </summary>
    //    /// <returns></returns>
    //    private long timeGen()
    //    {
    //        return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
    //    }

    //}
}
