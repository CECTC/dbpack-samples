FROM zhu733756/alpine-py3:3.14-gcc

WORKDIR /workdir
ADD . /workdir/

# RUN pip3 config set global.index-url http://mirrors.aliyun.com/pypi/simple
# RUN pip3 config set install.trusted-host mirrors.aliyun.com

RUN pip3 install -r /workdir/requirements.txt

EXPOSE 3001
ENTRYPOINT [ "python3", "/workdir/order/app.py"]
