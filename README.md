# dapr-demo
# 启动示例:
dapr run --app-id service-product --app-port 9002 --dapr-http-port 9003 -- dotnet ProductService.dll --urls "http://*:9002"
dapr run --app-id service-order --app-port 9000 --dapr-http-port 9001 -- dotnet OrderService.dll --urls "http://*:9000"
