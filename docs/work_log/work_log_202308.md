# 开发日志

## 2023-08 

### 01. ef core


##### 迁移

1.Add-Migration NewMigration  生成已有数据库初始化代码
2.Script-Migration 生成sql语句
3.Update-Database 

 参考： 

 > [.net如何优雅的使用EFCore ](https://www.cnblogs.com/qwqwQAQ/p/16932139.html)
 > [EF7创建模型值生成篇 ](https://www.cnblogs.com/YataoFeng/p/17187786.html)
 > [asp.net core EFCore 属性配置与DbContext]( https://juejin.cn/post/7095288086480814116#heading-8)


### 02. 程序集

1. AppDomain.CurrentDomain.GetAssemblies()	这个方法获取的是当前应用程序域已经加载的程序集，未加载的是获取不到的(尽管引用了该项目),所以在配置依赖注入时，可能会出现有些程序集拿不到的情况，导致没有注入所有需要的服务。
2. Assembly.GetEntryAssembly().GetReferencedAssemblies().Select(Assembly.Load) 获取该应用所有引用的程序集



 参考： 

 > [.netCore各种通过反射加载程序集的方法的总结](https://www.cnblogs.com/qwfy-y/p/14850297.html)


 
### 03. 模块化设计



 参考： 

 > [如何实现一个模块化方案](https://blog.csdn.net/kaixincheng2009/article/details/108675650)