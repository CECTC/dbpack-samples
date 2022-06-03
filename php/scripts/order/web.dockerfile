FROM nginx:1.10

ADD scripts/order/vhost.conf /etc/nginx/conf.d/default.conf
