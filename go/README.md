# dbpack-samples

### Step0: Clone dbpack and dbpack-samples
```shell
git clone git@github.com:cectc/dbpack.git
git clone git@github.com:cectc/dbpack-samples.git
cd dbpack-samples
```

### Step1: Setup etcd

### Step2: Setup mysql, initialize the database with the following sql script
```
./scripts/order.sql
./scripts/product.sql
```

### Step3: run dbpack
```bash
vim ./go/config1.yaml
# update distributed_transaction.etcd_config.endpoints

vim ./go/config2.yaml
# update data_source_cluster.dsn
# update distributed_transaction.etcd_config.endpoints

vim ./go/config3.yaml
# update data_source_cluster.dsn
# update distributed_transaction.etcd_config.endpoints

cd ../dbpack

make build-local

./dist/dbpack start --config ../dbpack-samples/go/config1.yaml

./dist/dbpack start --config ../dbpack-samples/go/config2.yaml

./dist/dbpack start --config ../dbpack-samples/go/config3.yaml
```

### Step4: setup aggregation_svc client
```bash
cd ../dbpack-samples/go/

go run aggregation_svc/main.go
```

### Step5: setup order_svc client
```bash
cd ../dbpack-samples/go/
vim ./order_svc/main.go
# update dsn

go run order_svc/main.go
```

### Step6: setup product_svc client
```bash
cd ../dbpack-samples/go/
vim ./product_svc/main.go
# update dsn

go run product_svc/main.go
```

### Step7: access and test
```
curl -XPOST http://localhost:13000/v1/order/create
```