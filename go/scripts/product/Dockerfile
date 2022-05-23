FROM golang:1.18 as builder
WORKDIR /app
ADD . /app
RUN make build-product-svc

FROM alpine:latest
WORKDIR /
COPY --from=builder /app/product_svc/ProductSvc /ProductSvc
CMD ["/ProductSvc"]