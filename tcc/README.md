# dbpack-samples

Simply, you can build the environment by running the `make run` command. then, you can test by
- `curl -XPOST http://localhost:8080/transfer-commit` for transaction commit test, or
- `curl -XPOST http://localhost:8080/transfer-commit` for transaction rollback test.

After executing `curl -XPOST http://localhost:8080/transfer-commit`, check log by `docker logs dbpack1` 
or `docker logs dbpack2`, then, you will see confirm interface be called automatically.

After executing `curl -XPOST http://localhost:8080/transfer-cancel`, check log by `docker logs dbpack1`
or `docker logs dbpack2`, then, you will see cancel API been called automatically.
