package main

import (
	"log"
	"net/http"

	"github.com/gin-gonic/gin"
)

func main() {
	r := gin.Default()

	r.POST("/prepare", func(c *gin.Context) {
		req := &struct {
			AccountFrom string `json:"from"`
			AccountTo   string `json:"to"`
			Amount      int64  `json:"amount"`
		}{}
		if err := c.ShouldBindJSON(&req); err != nil {
			c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
			return
		}
		amount := float64(req.Amount) / float64(1000)
		log.Default().Printf("Prepared account %s sub: %.2f", req.AccountFrom, amount)
		c.JSON(200, gin.H{
			"success": true,
		})
	})

	r.POST("/confirm", func(c *gin.Context) {
		req := &struct {
			AccountFrom string `json:"from"`
			AccountTo   string `json:"to"`
			Amount      int64  `json:"amount"`
		}{}
		if err := c.ShouldBindJSON(&req); err != nil {
			c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
			return
		}
		amount := float64(req.Amount) / float64(1000)
		log.Default().Printf("Confirmed account %s sub: %.2f", req.AccountFrom, amount)
		c.JSON(200, gin.H{
			"success": true,
		})
	})

	r.POST("/cancel", func(c *gin.Context) {
		req := &struct {
			AccountFrom string `json:"from"`
			AccountTo   string `json:"to"`
			Amount      int64  `json:"amount"`
		}{}
		if err := c.ShouldBindJSON(&req); err != nil {
			c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
			return
		}
		amount := float64(req.Amount) / float64(1000)
		log.Default().Printf("Canceled account %s sub: %.2f", req.AccountFrom, amount)
		c.JSON(200, gin.H{
			"success": true,
		})
	})
	r.Run(":8081")
}
