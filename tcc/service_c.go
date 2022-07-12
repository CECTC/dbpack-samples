package main

import (
	"bytes"
	"encoding/json"
	"net/http"

	"github.com/gin-gonic/gin"
	"github.com/pkg/errors"
)

const (
	transferServiceA = "http://dbpack1:8080/prepare"
	transferServiceB = "http://dbpack2:8080/prepare"
)

func main() {
	r := gin.Default()

	r.POST("/transfer-commit", func(c *gin.Context) {
		xid := c.GetHeader("x-dbpack-xid")
		err := callServiceA(xid)
		if err != nil {
			c.JSON(400, gin.H{
				"success": false,
				"message": "transfer failed",
			})
		}
		err = callServiceB(xid)
		if err != nil {
			c.JSON(400, gin.H{
				"success": false,
				"message": "transfer failed",
			})
		}

		// The confirm interface will be called automatically
		c.JSON(200, gin.H{
			"success": true,
			"message": "success",
		})
	})

	r.POST("/transfer-cancel", func(c *gin.Context) {
		xid := c.GetHeader("x-dbpack-xid")
		err := callServiceA(xid)
		if err != nil {
			c.JSON(400, gin.H{
				"success": false,
				"message": "transfer failed",
			})
		}
		err = callServiceB(xid)
		if err != nil {
			c.JSON(400, gin.H{
				"success": false,
				"message": "transfer failed",
			})
		}

		// The cancel interface will be called automatically
		c.JSON(200, gin.H{
			"success": true,
			"message": "success",
		})
	})

	r.Run(":8083")
}

func callServiceA(xid string) error {
	req := &struct {
		AccountFrom string `json:"from"`
		AccountTo   string `json:"to"`
		Amount      int64  `json:"amount"`
	}{
		AccountFrom: "scott",
		AccountTo:   "jane",
		Amount:      1000000,
	}
	reqData, err := json.Marshal(req)
	transferReq, err := http.NewRequest("POST", transferServiceA, bytes.NewBuffer(reqData))
	if err != nil {
		panic(err)
	}
	transferReq.Header.Set("Content-Type", "application/json")
	transferReq.Header.Set("x-dbpack-xid", xid)

	client := &http.Client{}
	result, err := client.Do(transferReq)
	if err != nil {
		return err
	}
	if result.StatusCode != http.StatusOK {
		return errors.Errorf("%s", result.Body)
	}
	return nil
}

func callServiceB(xid string) error {
	req := &struct {
		AccountFrom string `json:"from"`
		AccountTo   string `json:"to"`
		Amount      int64  `json:"amount"`
	}{
		AccountFrom: "scott",
		AccountTo:   "jane",
		Amount:      1000000,
	}
	reqData, err := json.Marshal(req)
	transferReq, err := http.NewRequest("POST", transferServiceB, bytes.NewBuffer(reqData))
	if err != nil {
		panic(err)
	}
	transferReq.Header.Set("Content-Type", "application/json")
	transferReq.Header.Set("x-dbpack-xid", xid)

	client := &http.Client{}
	result, err := client.Do(transferReq)
	if err != nil {
		return err
	}
	if result.StatusCode != http.StatusOK {
		return errors.Errorf("%s", result.Body)
	}
	return nil
}
