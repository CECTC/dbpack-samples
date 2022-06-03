# dbpack-samples

Simply, you can build the environment by running the `make run` command. then, you can test by
- `curl -XPOST http://localhost:13000/v1/order/create` for transaction commit test, or
- `curl -XPOST http://localhost:13000/v1/order/create2` for transaction rollback test.

Then check whether the database data is consistent.  
Alternatively, you can manually build the environment according to the following steps.

### Step0: Clone dbpack and dbpack-samples
```shell
git clone git@github.com:cectc/dbpack.git
git clone git@github.com:cectc/dbpack-samples.git
cd dbpack-samples
```

### Step1: Setup etcd, you can deploy etcd with docker.
```shell
docker run -d --name etcd-server \
    --publish 2379:2379 \
    --publish 2380:2380 \
    --env ALLOW_NONE_AUTHENTICATION=yes \
    --env ETCD_ADVERTISE_CLIENT_URLS=http://etcd-server:2379 \
    bitnami/etcd:latest
```

### Step2: Setup mysql, initialize the database with the following sql script
```
./scripts/order.sql
./scripts/product.sql
```

### Step3: run dbpack
```bash
vim ./configs/config-aggregation.yaml
# update distributed_transaction.etcd_config.endpoints

vim ./configs/config-product.yaml
# update data_source_cluster.dsn
# update distributed_transaction.etcd_config.endpoints

vim ./configs/config-order.yaml
# update data_source_cluster.dsn
# update distributed_transaction.etcd_config.endpoints

cd ../dbpack

make build-local

./dist/dbpack start --config ../dbpack-samples/configs/config-aggregation.yaml

./dist/dbpack start --config ../dbpack-samples/configs/config-product.yaml

./dist/dbpack start --config ../dbpack-samples/configs/config-order.yaml
```

### Step4: setup order_svc client
```bash
cd ../dbpack-samples/java/order
vim ./src/main/resources/application.properties
# update spring.datasource.url

mvnw package
java -jar target/order-0.0.1-SNAPSHOT.jar
```

### Step5: setup product_svc client
```bash
cd ../dbpack-samples/java/product
vim ./src/main/resources/application.properties
# update spring.datasource.url

mvnw package
java -jar target/product-0.0.1-SNAPSHOT.jar
```


### Step6: setup aggregation_svc client
```bash
cd ../dbpack-samples/java/aggregation
vim ./src/main/java/com/cectc/dbpack/aggregation/service/OrderService.java
# update createSoUrl allocateInventoryUrl

mvnw package
java -jar target/aggregation-0.0.1-SNAPSHOT.jar
```

### Step7: access and test
```
curl -XPOST http://localhost:13000/v1/order/create
```