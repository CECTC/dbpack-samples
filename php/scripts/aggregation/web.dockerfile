FROM nginx:1.10

ADD scripts/aggregation/vhost.conf /etc/nginx/conf.d/default.conf
