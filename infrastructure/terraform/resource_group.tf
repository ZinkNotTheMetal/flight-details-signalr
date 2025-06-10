resource "azurerm_resource_group" "rg_signalr" {
  name     = "rg-signalr-${var.environment}"
  location = var.location
}
