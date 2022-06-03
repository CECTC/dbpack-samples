FROM nginx:1.10

ADD scripts/product/vhost.conf /etc/nginx/conf.d/default.conf
