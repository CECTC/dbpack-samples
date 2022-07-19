FROM maven:3.5.0-jdk-8-alpine AS builder

# add pom.xml and source code
ADD ./aggregation/pom.xml pom.xml
ADD ./aggregation/src src/

# package jar
RUN mvn clean package

# Second stage: minimal runtime environment
From openjdk:8-jre-alpine

# copy jar from the first stage
COPY --from=builder target/aggregation-0.0.1-SNAPSHOT.jar aggregation-0.0.1-SNAPSHOT.jar

EXPOSE 3000

CMD ["java", "-jar", "aggregation-0.0.1-SNAPSHOT.jar"]
