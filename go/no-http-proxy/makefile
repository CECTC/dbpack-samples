build-aggregation-svc:
	GOOS="linux"  GOARCH="amd64" CGO_ENABLED=0 go build -o aggregation_svc/AggregationSvc ./aggregation_svc

build-order-svc:
	GOOS="linux"  GOARCH="amd64" CGO_ENABLED=0 go build -o order_svc/OrderSvc ./order_svc

build-product-svc:
	GOOS="linux"  GOARCH="amd64" CGO_ENABLED=0 go build -o product_svc/ProductSvc ./product_svc

run:
	docker-compose -f docker-compose.yaml up --build -dcu