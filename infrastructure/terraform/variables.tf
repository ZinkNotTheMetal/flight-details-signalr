variable "environment" {
  type        = string
  description = "Environment that the application lives in"
}

variable "subscription_id" {
  type        = string
  description = "This is required for the latest version of the Azure provider"
}

variable "location" {
  type        = string
  description = "This is the location of the resource and where it lives within Azure"
}

variable "signalr_config" {
  type = object({
    sku_name = string
    capacity = number
  })
}