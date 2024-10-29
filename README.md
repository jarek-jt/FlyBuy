## About
___

*FlyBuy* is a plane factory where you can design your dream
plane by selecting and customizing various elements.
Once your design is complete, you'll receive a final price,
and the plane will be prepared for production.

## Installation
___

### EventStoreDb
```
docker run --name esdb-node -it -p 2113:2113 \
    eventstore/eventstore:24.6 --insecure --run-projections=All
    --enable-atom-pub-over-http
```
or using docker-compose `docker-compose.yaml`, just run:
`docker-compose up`

* Admin portal url: http://localhost:2113/
* Event store navigator: https://learn.eventstore.com/event-store-navigator-preview

## Notes
___

* All prefixes (such as company and product names) were removed from project names
to better visualize the implemented concept

## High Level Design
___

![HLD](/resources/HLD.png)