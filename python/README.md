# dbpack-samples

## Step0: Clone dbpack and dbpack-samples
```shell
git clone git@github.com:cectc/dbpack.git
git clone git@github.com:cectc/dbpack-samples.git
cd dbpack-samples/python
```

## Step1: Manual startup

### Step1.1: Setup etcd
```shell
docker run -d --name etcd-server \
    --publish 2379:2379 \
    --publish 2380:2380 \
    --env ALLOW_NONE_AUTHENTICATION=yes \
    --env ETCD_ADVERTISE_CLIENT_URLS=http://etcd-server:2379 \
    bitnami/etcd:latest
```

### Step1.2: Setup mysql, initialize the database with the following sql script
```
./scripts/order.sql
./scripts/product.sql
```

### Step1.3: run dbpack
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

### Step1.4: setup python requirements
```bash
cd ../dbpack-samples/python
pip3 install -r requirements.txt
```

### Step1.5: setup aggregation client
```bash
cd ../dbpack-samples/python/aggregation

python3 app.py
```

### Step1.6: setup order client
```bash
cd ../dbpack-samples/python/order

python3 app.py
```

### Step1.7: setup product client
```bash
cd ../dbpack-samples/python/product

python3 app.py
```

## Step2: Automatic startup
```bash
cd ../dbpack-samples/python

docker-compose up --build
```

## Step3: access and test
```
curl -XPOST http://localhost:13000/v1/order/create
```