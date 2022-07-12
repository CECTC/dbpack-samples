FROM golang:1.18 as builder
ARG GOPROXY="https://goproxy.io"
WORKDIR /app
ADD go.mod /app
ADD go.sum /app
ADD service_b.go /app
# cache deps before building and copying source so that we don't need to re-download as much
# and so that source changes don't invalidate our downloaded layer
RUN GOPROXY=$GOPROXY go mod download
RUN GOOS="linux"  GOARCH="amd64" CGO_ENABLED=0 go build service_b.go

FROM alpine:latest
WORKDIR /
COPY --from=builder /app/service_b /service_b
CMD ["/service_b"]
