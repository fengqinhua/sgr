

**说明**

# 一、git 子模块

如果某个工作中的项目需要包含并使用另一个项目。 也许是第三方库，或者你独立开发的，用于多个父项目的库，此时可使用git子模块。

方法：

1. 添加子模块： git submodule add https://github.com/xxx/yyy 
2. 克隆含有子模块的项目 ： 当使用git clone 克隆一个含有子模块的项目时，默认会包含该子模块目录，但其中还没有任何文件。你必须运行两个命令：git submodule init 用来初始化本地配置文件，而 git submodule update 则从该项目中抓取所有数据并检出父项目中列出的合适的提交。
另外，还有一种更简单的方式， 如果给 git clone 命令传递 --recurse-submodules 选项，它就会自动初始化并更新仓库中的每一个子模块， 包括可能存在的嵌套子模块。
 
 参考： https://git-scm.com/book/zh/v2/Git-%E5%B7%A5%E5%85%B7-%E5%AD%90%E6%A8%A1%E5%9D%97