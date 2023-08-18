# 开发日志

## 2023-07 

### 01. git 子模块

如果某个工作中的项目需要包含并使用另一个项目。 也许是第三方库，或者你独立开发的，用于多个父项目的库，此时可使用git子模块。

方法：

1. 添加子模块： git submodule add https://github.com/xxx/yyy 
2. 克隆含有子模块的项目 ： 当使用git clone 克隆一个含有子模块的项目时，默认会包含该子模块目录，但其中还没有任何文件。你必须运行两个命令：git submodule init 用来初始化本地配置文件，而 git submodule update 则从该项目中抓取所有数据并检出父项目中列出的合适的提交。
另外，还有一种更简单的方式， 如果给 git clone 命令传递 --recurse-submodules 选项，它就会自动初始化并更新仓库中的每一个子模块， 包括可能存在的嵌套子模块。
 
 参考： 

 > [Git 工具 - 子模块]( https://git-scm.com/book/zh/v2/Git-%E5%B7%A5%E5%85%B7-%E5%AD%90%E6%A8%A1%E5%9D%97)

### 02. VS类增加默认的注释信息

方法：

1. 寻找VS中配置类模板的目录，例如我的就在：C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\IDE\ItemTemplates\CSharp\Code\2052\Class
2. 打开目录中的Class.cs文件进行修改，增加注释信息，如下：
~~~code
/**************************************************************
 * 
 * 唯一标识：$guid10$
 * 命名空间：$rootnamespace$
 * 创建时间：$time$
 * 机器名称：$machinename$
 * 创建者：$username$
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

 
using System;
using System.Collections.Generic;
$if$ ($targetframeworkversion$ >= 3.5)using System.Linq;
$endif$using System.Text;
$if$ ($targetframeworkversion$ >= 4.5)using System.Threading.Tasks;
$endif$
namespace $rootnamespace$
{
    class $safeitemrootname$
    {
    }
}
~~~
3. 同样的方式修改Interface模板文件

### 03. 配置 GitHub 和 Gitee 开发账号

具体可参考：

> [生成和添加 SSH 公钥](https://www.cnblogs.com/librarookie/p/15390170.html)  
> [配置 GitHub 和 Gitee 共存环境](https://cloud.tencent.com/document/product/436/7778)  


### 04. C# 8.0 可空(Nullable) 与 泛型约束

1.不可为null的值类型

~~~code
public static void GenericStruct<T>(T t) where T : struct{ }
~~~

2.不可为null的引用类型：where T : class

~~~code
public static void GenericClass<T>(T t) where T : class{ }
~~~

3.可为null的引用类型：where T : class?

~~~code
public static void GenericClassNullable<T>(T t) where T : class?{ }
~~~

4.不可为null：where T : notnull

~~~code
public static void GenericNotnull<T>(T t) where T : notnull{ }
~~~

具体可参考：

> [C#高级编程之泛型（二）：泛型约束](https://blog.51cto.com/u_15127615/2755478)  
> [C# 8.0 可空(Nullable)给ASP.NET Core带来的坑](https://blog.csdn.net/q913777031/article/details/113186007)  
> [C# 8.0 可空引用类型](https://www.cnblogs.com/MASA/p/15836687.html)
> [C# 8.0 的可空引用类型，不止是加个问号哦！你还有很多种不同的可空玩法](https://blog.csdn.net/wpwalter/article/details/103760542)


### 05. 仓储和工作单元的设计思考



具体可参考：

> [Repository 简化实现多条件查询](https://www.cnblogs.com/xishuai/p/repository-query-linq-expression.html)  


### 06. Endpoints



具体可参考：

> [MinimalApi.Endpoint](https://github.com/michelcedric/StructuredMinimalApi)
> [FastEndpoints](https://github.com/FastEndpoints)
> [Ardalis.ApiEndpoints](https://github.com/ardalis/ApiEndpoints)


### 07.AsyncLocal

1. AsyncLocal 本身不保存数据，数据保存在 ExecutionContext 实例的 m_localValues 的私有字段上，字段类型定义是 IAsyncLocalMap ，以 IAsyncLocal => object 的 Map 结构进行保存，且实现类型随着元素数量的变化而变化。
2. ExecutionContext 实例 保存在 Thread.CurrentThread._executionContext 上，实现与当前线程的关联。
3. 对于 IAsyncLocalMap 的实现类，如果 AsyncLocal 注册了回调，value 传 null 不会被忽略。
4. 没注册回调时分为两种情况：如果 key 存在，则做删除处理，map 类型可能出现降级。如果 key 不存在，则直接忽略。
5. ExecutionContext 和 IAsyncLocalMap 的实现类都被设计成不可变(immutable)。同一个 key 前后两次 value 发生变化后，会产生新的 ExecutionContext 的实例和 IAsyncLocalMap 实现类实例。
6. ExecutionContext 与当前线程绑定，默认流动到辅助线程，可以禁止流动和恢复流动，且禁止流动仅影响当前线程向其辅助线程的传递，不影响后续。

参考资料：  
[1. 浅析 .NET 中 AsyncLocal 的实现原理](https://www.cnblogs.com/eventhorizon/p/12240767.html)


### 08. System.Text.Json
   

   
参考资料：  
[1. System.Text.Json 常规用法](https://www.cnblogs.com/RainFate/p/15720684.html)   
[2. How to use JsonNode to read, write, and modify JSON](https://makolyte.com/csharp-how-to-use-jsonnode-to-read-write-and-modify-json/#Remove_a_property)      

### 09. 线程安全的 RandomHelper

1. Random中的方法并非线程安全的，在多线程的情况下可能会存在返回多个重复值的情况。解决方案：采用[ThreadStatic]标记Random，确保每个线程中的Random对象不一样
~~~code
        [ThreadStatic]
        private static Random _local;
~~~

2. Random 返回值的随机性由创建对象时的种子seed决定 。 解决方法：采用一个全局的随机对象来生成种子。之前采用 RNGCryptoServiceProvider ，后考虑性能换成 Random

~~~code
        //private static RNGCryptoServiceProvider _global = new RNGCryptoServiceProvider();
        private static Random _global = new Random();
        private static Random Instance
        {
            get
            {
                if (_local is null)
                {
                    int seed;
                    lock (_global) // 👈 Ensure no concurrent access to Global
                    {
                        //byte[] buffer = new byte[4];
                        //_global.GetBytes(buffer);
                        //seed = BitConverter.ToInt32(buffer, 0);
                        seed = _global.Next();
                    }

                    _local = new Random(seed); // 👈 Create [ThreadStatic] instance with specific seed
                }

                return _local;
            }
        }
~~~

参考资料：   
[1. Getting random numbers in a thread-safe way](https://devblogs.microsoft.com/pfxteam/getting-random-numbers-in-a-thread-safe-way/)  
[2. Working with System.Random and threads safely in .NET Core and .NET Framework](https://andrewlock.net/building-a-thread-safe-random-implementation-for-dotnet-framework/)  
      
