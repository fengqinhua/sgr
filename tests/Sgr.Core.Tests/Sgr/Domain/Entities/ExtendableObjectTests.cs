/**************************************************************
 * 
 * 唯一标识：1e601a11-9d83-4f4a-936a-d287a8edf6fe
 * 命名空间：Sgr.Domain.Entities
 * 创建时间：2023/7/25 17:32:28
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Exceptions;
using System.Text;

namespace Sgr.Domain.Entities
{
    [TestClass]
    public class ExtendableObjectTests
    {
        [TestMethod]
        public void ExtendableObject_SaveValue()
        {
            var entity = new ExtendableEntityClass();

            //针对数值

            int old_int = 1024;

            //移除不存在的值
            Assert.IsFalse(entity.RemoveValue("test_int"));

            //设置后再读取数值
            entity.SetValue("test_int", old_int);
            Assert.AreEqual(entity.GetValue<int>("test_int"), old_int);
            Assert.AreEqual(entity.GetString("test_int"), "");

            //移除已存在的值
            Assert.IsTrue(entity.RemoveValue("test_int"));
            Assert.AreEqual(entity.GetValue<int>("test_int"), 0);

            //返回默认值
            Assert.AreEqual(entity.GetValue<int>("test_int_2"), 0);
        }

        [TestMethod]
        public void ExtendableObject_SaveString()
        {
            var entity = new ExtendableEntityClass();

            //针对数值

            string old_string = "mapleleaf";

            //移除不存在的值
            Assert.IsFalse(entity.RemoveObject("test_string"));

            //设置后再读取数值
            entity.SetString("test_string", old_string);
            Assert.AreEqual(entity.GetString("test_string"), old_string);

            //移除已存在的值
            Assert.IsTrue(entity.RemoveObject("test_string"));
            Assert.AreEqual(entity.GetString("test_string"), "");

            //返回默认值
            Assert.AreEqual(entity.GetString("test_string_2"), "");
        }

        [TestMethod]
        public void ExtendableObject_SaveObject()
        {
            var entity = new ExtendableEntityClass();

            //针对对象
            var old_student = new Student()
            {
                Id = 9527,
                Name = "李天来",
                Six = Six.Men
            };

            //移除不存在的值
            Assert.IsFalse(entity.RemoveObject("test_object"));

            //设置后再读取数值
            entity.SetObject("test_object", old_student);
            var read_student = entity.GetObject<Student>("test_object");
            equalStudent(old_student, read_student);

            //移除已存在的值
            Assert.IsTrue(entity.RemoveObject("test_object"));
            Assert.AreEqual(entity.GetObject<Student>("test_object"), null);

            //返回默认值
            Assert.AreEqual(entity.GetObject<Student>("test_object_2"), null);
        }

        [TestMethod]
        public void ExtendableObject_UserFriendlyException()
        {
            var entity = new ExtendableEntityClass();

            try
            {
                for(int i = 0; i < 100; i++)
                {
                    //针对对象
                    var old_student = new Student()
                    {
                        Id = 9527,
                        Name = "李天来",
                        Six = Six.Men
                    };

                    entity.SetObject($"test_object_{i}", old_student);
                }
            }
            catch(UserFriendlyException ex)
            {
                Assert.IsTrue(ex.Message.Contains("ExtensionData exceeding the maximum number of bytes"));
            }
        }


        private static void equalStudent(Student old_student, Student? read_student)
        {
            Assert.IsNotNull(read_student);
            Assert.AreEqual(read_student.Id, old_student.Id);
            Assert.AreEqual(read_student.Name, old_student.Name);
            Assert.AreEqual(read_student.Six, old_student.Six);
            Assert.AreEqual(read_student.Address, old_student.Address);
        }
    }

    enum Six
    {
        Men, Women
    }

    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Address { get; set; }
        public Six Six { get; set; }
    }

    class ExtendableEntityClass : IExtendableObject
    {
        public string? ExtensionData { get; set; }
    }
}
