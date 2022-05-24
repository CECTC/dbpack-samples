# dbpack-samples

Simply, you can build the environment by running the `make run` command. then, you can test by `curl -XPOST http://localhost:13000/v1/order/create
`, check whether the database data is consistent. Or, you can manually build the environment according to the following steps.

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
vim ./configs/config1.yaml
# update distributed_transaction.etcd_config.endpoints

vim ./configs/config2.yaml
# update data_source_cluster.dsn
# update distributed_transaction.etcd_config.endpoints

vim ./configs/config3.yaml
# update data_source_cluster.dsn
# update distributed_transaction.etcd_config.endpoints

cd ../dbpack

make build-local

./dist/dbpack start --config ../dbpack-samples/configs/config1.yaml

./dist/dbpack start --config ../dbpack-samples/configs/config2.yaml

./dist/dbpack start --config ../dbpack-samples/configs/config3.yaml
```

### Step4: setup order_svc client
```bash
cd ../dbpack-samples/go/
vim ./order_svc/main.go
# update dsn

go run order_svc/main.go
```

### Step5: setup product_svc client
```bash
cd ../dbpack-samples/go/
vim ./product_svc/main.go
# update dsn

go run product_svc/main.go
```

### Step6: setup aggregation_svc client
```bash
cd ../dbpack-samples/go/
vim ./aggregation_svc/svc/svc.go
# update createSoUrl updateInventoryUrl

go run aggregation_svc/main.go
```

### Step7: access and test
```
curl -XPOST http://localhost:13000/v1/order/create
```
Check whether the database data is consistent.