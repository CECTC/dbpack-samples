This directory contains two go samples with the following deployment structure:

+ `with-http-proxy` uses DBPack to proxy http requests, injecting `x-dbpack-xid` and `traceparent` into the request header.

  ![with-http-proxy](https://github.com/CECTC/dbpack-doc/blob/master/images/go-sample1.png)

+ `no-http-proxy` uses gin middleware instead of DBPack proxy, adds the logic of beginning, committing, and rolling back global transactions in gin middleware, and injects `xid` into gin context after beginning global transaction.

  ![no-http-proxy](https://github.com/CECTC/dbpack-doc/blob/master/images/go-sample2.png)

---
本目录包含两个 go 示例，它们的部署结构如下：

+ `with-http-proxy` 使用 DBPack 代理 http 请求，注入 `x-dbpack-xid` 和 `traceparent` 到 request header 中。

  ![with-http-proxy](https://github.com/CECTC/dbpack-doc/blob/master/images/go-sample1.png)

+ `no-http-proxy` 使用 gin middleware 代替 DBPack 代理，在 gin middleware 中加入全局事务的开启、提交、回滚逻辑，开启全局事务后，将 xid 注入 gin context。

  ![no-http-proxy](https://github.com/CECTC/dbpack-doc/blob/master/images/go-sample2.png)
