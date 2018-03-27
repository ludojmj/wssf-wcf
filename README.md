# wssf-wcf
.NET 4.6.2 Web Service WCF

Initiated with https://github.com/Phidiax/open-wssf-2015

## Data contract and service contract
- ./Schemas..xsd
- ./Services.xsd

### Faults, Service and Host modeling 
- ./WCFTemplate.Model/WCFTemplate.datacontract (_Faults_)
- ./WCFTemplate.Model/WCFTemplate.servicecontract (_Operations_)
- ./WCFTemplate.Model/WCFTemplate.host

## Generated and overriden classes
- ./WCFTemplate.Src/*

## Deployment
- ./WCFTemplate.Client/* (_Rest Json WebApi2_)
- ./WCFTemplate.Host/* (_SOAP WCF Web Service _)

## Client Testing
- http://localhost/WCFTemplate.Client/index.html
- http://localhost/WCFTemplate.Client/swagger

## Web Api Testing
- http://localhost/WCFTemplate.Client/api/operation

## Web Service Testing
- http://localhost/WCFTemplate.Host/ServiceTemplate.svc?singleWsdl
