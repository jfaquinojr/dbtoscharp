# dbtoscharp

Generate POCOs from your existing SQL Server database all while on the comfort of the command line.

![dbtocs](https://i.imgur.com/XKDi7q7.png)

I have a legacy application that I am excited to rewrite into .NET Core and I thought dbtosc is something I want to use.

_version 0.0.0._ This is something I whipped up very quickly this mean lots of bugs and missing features. You're free to use this but don't blame me if your house burns down or your wife leaves you.

### In the works:
Subcommand to generate multiple classes from SQL Server tables and views
```
$ dbtocs multi 
```



### This uses the following libraries
- [CliFX](https://github.com/Tyrrrz/CliFx)
- [Dapper](https://github.com/StackExchange/Dapper)
- [xUnit](https://github.com/xunit/xunit)
