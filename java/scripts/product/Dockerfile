FROM maven:3.5.0-jdk-8-alpine AS builder

# add pom.xml and source code
ADD ./product/pom.xml pom.xml
ADD ./product/src src/

# package jar
RUN mvn clean package

# Second stage: minimal runtime environment
From openjdk:8-jre-alpine

# copy jar from the first stage
COPY --from=builder target/product-0.0.1-SNAPSHOT.jar product-0.0.1-SNAPSHOT.jar

EXPOSE 3002

CMD ["java", "-jar", "product-0.0.1-SNAPSHOT.jar"]