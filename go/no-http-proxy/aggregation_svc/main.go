/*
 * Copyright 2022 CECTC, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

package main

import (
	"log"

	"github.com/cectc/dbpack/pkg/config"
	dbpackGin "github.com/cectc/dbpack/pkg/contrib/gin"
	"github.com/cectc/dbpack/pkg/dt"
	"github.com/gin-gonic/gin"
	"go.etcd.io/etcd/client/v3"

	"github.com/dbpack/dbpack-samples/aggregation_svc/svc"
)

func main() {
	dt.InitDistributedTransactionManager(&config.DistributedTransaction{
		ApplicationID:                    "aggregation-svc",
		RetryDeadThreshold:               130000,
		RollbackRetryTimeoutUnlockEnable: true,
		EtcdConfig: clientv3.Config{
			Endpoints: []string{"etcd:2379"},
		},
	})

	r := gin.Default()

	r.POST("/v1/order/create", dbpackGin.GlobalTransaction(60000), func(c *gin.Context) {
		xid := c.Value(dbpackGin.XID)
		err := svc.GetSvc().CreateSo(c, xid.(string), false)
		if err != nil {
			log.Default().Println(err)
			c.JSON(400, gin.H{
				"success": false,
				"message": "fail",
			})
		} else {
			c.JSON(200, gin.H{
				"success": true,
				"message": "success",
			})
		}
	})

	r.POST("/v1/order/create2", dbpackGin.GlobalTransaction(60000), func(c *gin.Context) {
		xid := c.Value(dbpackGin.XID)
		err := svc.GetSvc().CreateSo(c, xid.(string), true)
		if err != nil {
			log.Default().Println(err)
			c.JSON(400, gin.H{
				"success": false,
				"message": "fail",
			})
		} else {
			c.JSON(200, gin.H{
				"success": true,
				"message": "success",
			})
		}
	})

	r.Run(":3000")
}
