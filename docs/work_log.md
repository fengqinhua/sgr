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


### C# 8.0 可空(Nullable) 与 泛型约束

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
