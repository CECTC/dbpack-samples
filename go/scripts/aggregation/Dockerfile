FROM golang:1.18 as builder
WORKDIR /app
ADD . /app
RUN make build-aggregation-svc

FROM alpine:latest
WORKDIR /
COPY --from=builder /app/aggregation_svc/AggregationSvc /AggregationSvc
CMD ["/AggregationSvc"]