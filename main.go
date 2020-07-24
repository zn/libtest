package main

import (
	"fmt"
	"path/filepath"
)

func main() {
	path, _ := filepath.Abs("~/")
	fmt.Println(path)
}
