# 开发日志

## 2023-08 

### 01. ef core


##### 迁移

1.Add-Migration -Name Sgr0815 -OutputDir Infrastructure/Migrations  生成已有数据库初始化代码
2.Script-Migration -Output DataBase/mysql_sgr0815.sql  生成sql语句
3.Remove-Migration
3.Update-Database 

参考： 

> [.net如何优雅的使用EFCore ](https://www.cnblogs.com/qwqwQAQ/p/16932139.html)
> [EF7创建模型值生成篇 ](https://www.cnblogs.com/YataoFeng/p/17187786.html)
> [asp.net core EFCore 属性配置与DbContext]( https://juejin.cn/post/7095288086480814116#heading-8)
> [Entity Framework Core 工具参考](https://learn.microsoft.com/zh-cn/ef/core/cli/powershell#add-migration)



### 02. 程序集

1. AppDomain.CurrentDomain.GetAssemblies()	这个方法获取的是当前应用程序域已经加载的程序集，未加载的是获取不到的(尽管引用了该项目),所以在配置依赖注入时，可能会出现有些程序集拿不到的情况，导致没有注入所有需要的服务。
2. Assembly.GetEntryAssembly().GetReferencedAssemblies().Select(Assembly.Load) 获取该应用所有引用的程序集



参考： 

> [.netCore各种通过反射加载程序集的方法的总结](https://www.cnblogs.com/qwfy-y/p/14850297.html)


 
### 03. 模块化设计



参考： 

> [如何实现一个模块化方案](https://blog.csdn.net/kaixincheng2009/article/details/108675650)


### 04. 中间件及拦截器

1. 中间件的三种定义方式
* Use方式： 
* 约定方式：
* 实现IMiddleware接口的方式
	
2. 中间件生命周期
* 实现IMiddleware接口的方式生命周期取决于自己注册服务实例时候声明的周期，而且这种方式没办法通过方法注入服务，因为有IMiddleware接口InvokeAsync方法的约束
* 基于约定的方式更灵活，它的声明周期是单例的，但是通过它的Invoke或InvokeAsync方法注入的服务实例生命周期是Scope的

3. ServiceFilter vs TypeFilter
* ServiceFilter和TypeFilter都实现了IFilterFactory
* ServiceFilter需要对自定义的Filter进行注册，TypeFilter不需要
* ServiceFilter的Filter生命周期源自于您如何注册，而TypeFilter每次都会创建一个新的实例

参考： 
> [ASP.NET Core中间件初始化探究](https://www.yii666.com/article/566691.html)
> [ASP .NET Core Api使用过滤器](https://www.cnblogs.com/RainFate/p/16946047.html)
> [Asp.net Core在Filter中使用依赖注入TypeFilter，ServiceFilter](https://blog.csdn.net/WuLex/article/details/122997041)
> [ASP.NET Core 中的筛选器](https://learn.microsoft.com/zh-cn/aspnet/core/mvc/controllers/filters?view=aspnetcore-7.0)
> [写入自定义 ASP.NET Core 中间件](https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/middleware/write?view=aspnetcore-7.0)



### 05. 依赖注入
 
1. 三种生命周期
* Transient ：每次请求时都会创建，并且永远不会被共享
* Scoped ： 在同一个Scope内只初始化一个实例 ，可以理解为（ 每一个request级别只创建一个实例，同一个http request会在一个 scope内）
* Singleton ：只会创建一个实例。该实例在需要它的所有组件之间共享。因此总是使用相同的实例
 
2. 建议做法
* 尽可能将您的服务注册为瞬态服务
* 谨慎使用Scoped，因为如果您创建子服务作用域或从非Web应用程序使用这些服务，则可能会非常棘手
* 谨慎使用Singleton ，因为您需要处理多线程和潜在的内存泄漏问题
* 服务不能依赖于生命周期小于其自身的服务 。 例如：如果将组件A注册为单例，则它不能依赖于使用Scoped或Transient生命周期注册的组件

参考： 
> [ASP.NET Core依赖注入——依赖注入最佳实践](https://www.cnblogs.com/runningsmallguo/p/10234307.html)


### 06. 异常处理


参考： 
> [ASP.NET core中使用UseExceptionHandler进行全局异常处理的实现](https://www.duidaima.com/Group/Topic/ASP.NET/2040)


### 07. 数据库 




参考： 
> [mysql有哪些索引类型](https://m.php.cn/faq/493277.html)
> [efcore 关系中的外键和主键](https://learn.microsoft.com/zh-cn/ef/core/modeling/relationships/foreign-and-principal-keys?source=recommendations)
> [Entity Framework Core Override Conventions](https://giridharprakash.me/2020/02/12/entity-framework-core-override-conventions/)
> [查看EFCore生成的语句的四种方式](https://blog.csdn.net/m0_66746443/article/details/124058262)
> [EF Core Like 模糊查询](https://www.cnblogs.com/xjxue/p/17558826.html)
> [EF Core 6 新功能汇总](https://mp.weixin.qq.com/s/VpqEWQPdEJUw_HHNeqBPdg)
> [EFCORE性能优化方案](https://www.cnblogs.com/lbonet/p/14608870.html)


### 08. 领域建模


参考： 

> [领域模型验证](https://blog.csdn.net/yan_yu_lv_ji/article/details/128753978)