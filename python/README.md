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
vim ./python/config1.yaml
# update distributed_transaction.etcd_config.endpoints

vim ./python/config2.yaml
# update data_source_cluster.dsn
# update distributed_transaction.etcd_config.endpoints

vim ./python/config3.yaml
# update data_source_cluster.dsn
# update distributed_transaction.etcd_config.endpoints

cd ../dbpack

make build-local

./dist/dbpack start --config ../dbpack-samples/python/config1.yaml

./dist/dbpack start --config ../dbpack-samples/python/config2.yaml

./dist/dbpack start --config ../dbpack-samples/python/config3.yaml
```

### Step4: setup python requirements
```bash
cd ../dbpack-samples/python
pip3 install -r requirements.txt
```

### Step5: setup aggregation client
```bash
cd ../dbpack-samples/python/aggregation

python3 app.py
```

### Step6: setup order client
```bash
cd ../dbpack-samples/python/order

python3 app.py
```

### Step7: setup product client
```bash
cd ../dbpack-samples/python/product

python3 app.py
```

### Step8: access and test
```
curl -XPOST http://localhost:13000/v1/order/create
```