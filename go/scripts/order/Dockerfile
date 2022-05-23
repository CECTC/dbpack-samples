FROM golang:1.18 as builder
WORKDIR /app
ADD . /app
RUN make build-order-svc

FROM alpine:latest
WORKDIR /
COPY --from=builder /app/order_svc/OrderSvc /OrderSvc
CMD ["/OrderSvc"]