FROM maven:3.5.0-jdk-8-alpine AS builder

# add pom.xml and source code
ADD ./order/pom.xml pom.xml
ADD ./order/src src/

# package jar
RUN mvn clean package

# Second stage: minimal runtime environment
From openjdk:8-jre-alpine

# copy jar from the first stage
COPY --from=builder target/order-0.0.1-SNAPSHOT.jar order-0.0.1-SNAPSHOT.jar

EXPOSE 3001

CMD ["java", "-jar", "order-0.0.1-SNAPSHOT.jar"]
