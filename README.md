# 项目简述

## 第一阶段目标

### Todo Module

模块功能：

1. Todo Task的创建、修改、删除等。

2. Todo统计分析，包括年、月度完成数量或进度，季度或月度分析。

3. Todo提醒功能，把未完成的todo通过Email进行消息提醒。

数据模型：

**Todo**

| Name                                 | Type         | Remark      |
|:------------------------------------ | ------------ | ----------- |
| TodoID                               | Int          | Primary Key |
| Content                              | String       |             |
| Status                               | TodoStatus   | Enum        |
| Category | TodoCategory | 1 : 1       |
| Active                               | bool         |             |
| CreateDate                           | Datetime     |             |
| CreateBy                             | Datetime     |             |
| UpdateDate                           | Datetime     |             |
| UpdateBy                             | Datetime     |             |

**Todo Category**

| Name           | Type       | Remark      |
| -------------- | ---------- | ----------- |
| TodoCategoryID | int        | Pramary Key |
| Name           | String     |             |
| Todos          | List<Todo> | 1 : N       |
| Active         | bool       |             |
| CreateDate     | Datetime   |             |
| CreateBy       | Datetime   |             |
| UpdateDate     | Datetime   |             |
| UpdateBy       | Datetime   |             |
