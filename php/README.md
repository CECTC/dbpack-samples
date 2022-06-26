# dbpack-samples for php

Simply, you can build the environment by running the `make run` command. then, you can test by 
- `curl -XPOST http://localhost:13000/v1/order/create` for transaction commit test, or
- `curl -XPOST http://localhost:13000/v1/order/create2` for transaction rollback test.

Then check whether the database data is consistent.  
Alternatively, you can manually build the environment according to the following steps.  

### Prerequisite
- php pdo extension is installed
- mysql database installed
- innodb table with primary key

### Step0: Clone dbpack and dbpack-samples
```shell
git clone git@github.com:cectc/dbpack.git
git clone git@github.com:cectc/dbpack-samples.git
cd dbpack-samples
```

### Step1: Setup etcd
```bash
ETCD_VER=v3.5.3

# choose either URL
GOOGLE_URL=https://storage.googleapis.com/etcd
GITHUB_URL=https://github.com/etcd-io/etcd/releases/download
DOWNLOAD_URL=${GOOGLE_URL}

rm -f /tmp/etcd-${ETCD_VER}-linux-amd64.tar.gz
rm -rf /tmp/etcd-download-test && mkdir -p /tmp/etcd-download-test

curl -L ${DOWNLOAD_URL}/${ETCD_VER}/etcd-${ETCD_VER}-linux-amd64.tar.gz -o /tmp/etcd-${ETCD_VER}-linux-amd64.tar.gz
tar xzvf /tmp/etcd-${ETCD_VER}-linux-amd64.tar.gz -C /tmp/etcd-download-test --strip-components=1
rm -f /tmp/etcd-${ETCD_VER}-linux-amd64.tar.gz

/tmp/etcd-download-test/etcd --version
/tmp/etcd-download-test/etcdctl version
/tmp/etcd-download-test/etcdutl version
```

### Step2: Setup mysql, initialize the database with the below sql script
```
./scripts/order.sql
./scripts/product.sql
```

### Step3: run dbpack
```bash
# update distributed_transaction.etcd_config.endpoints
# update filters && listeners
vim ./configs/config-aggregation.yaml

# update data_source_cluster.dsn
# update listeners
# update distributed_transaction.etcd_config.endpoints
vim ./configs/config-product.yaml

# update data_source_cluster.dsn
# update listeners
# update distributed_transaction.etcd_config.endpoints
vim ./configs/config-order.yaml

cd ../dbpack

# local build
make build-local
# production build
# make build

./dist/dbpack start --config ../dbpack-samples/configs/config-aggregation.yaml

./dist/dbpack start --config ../dbpack-samples/configs/config-product.yaml

./dist/dbpack start --config ../dbpack-samples/configs/config-order.yaml
```

### Step4: config vhost for services
```conf
# Nginx vhost config for aggregation service
server {
    listen 3000;
    index index.php index.html;
    root /path/to/the/aggregation_svc;

    location / {
        try_files $uri /index.php?$args;
    }

    location ~ \.php$ {
        fastcgi_split_path_info ^(.+\.php)(/.+)$;
        fastcgi_pass localhost:9000; # config for php-fpm
        fastcgi_index index.php;
        include fastcgi_params;
        fastcgi_param SCRIPT_FILENAME $document_root$fastcgi_script_name;
        fastcgi_param PATH_INFO $fastcgi_path_info;
    }
}
# Nginx vhost config for order service
server {
    listen 3001;
    index index.php index.html;
    root /path/to/the/order_svc;

    location / {
        try_files $uri /index.php?$args;
    }

    location ~ \.php$ {
        fastcgi_split_path_info ^(.+\.php)(/.+)$;
        fastcgi_pass localhost:9000; # config for php-fpm
        fastcgi_index index.php;
        include fastcgi_params;
        fastcgi_param SCRIPT_FILENAME $document_root$fastcgi_script_name;
        fastcgi_param PATH_INFO $fastcgi_path_info;
    }
}
# Nginx vhost config for product service
server {
    listen 3002;
    index index.php index.html;
    root /path/to/the/product_svc;

    location / {
        try_files $uri /index.php?$args;
    }

    location ~ \.php$ {
        fastcgi_split_path_info ^(.+\.php)(/.+)$;
        fastcgi_pass localhost:9000; # config for php-fpm
        fastcgi_index index.php;
        include fastcgi_params;
        fastcgi_param SCRIPT_FILENAME $document_root$fastcgi_script_name;
        fastcgi_param PATH_INFO $fastcgi_path_info;
    }
}
```

### Step5: access and test
```
# commit test
curl -XPOST http://localhost:13000/v1/order/create

# rollback test
curl -XPOST http://localhost:13000/v1/order/create2
```