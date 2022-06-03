FROM php:7.4-fpm

RUN rm /etc/apt/preferences.d/no-debian-php
RUN apt-get update && apt-get install -y libmcrypt-dev iputils-ping libzip-dev \
    libcurl4-openssl-dev default-mysql-client libmagickwand-dev --no-install-recommends
RUN docker-php-ext-install curl pdo_mysql zip

# Custom php.ini
COPY scripts/product/php.ini /usr/local/etc/php/conf.d/php.ini

CMD ["php-fpm"]
