# Terraform Documentation:
# https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/signalr_service
resource "azurerm_signalr_service" "signalr_service" {
  name                = "signalr-${var.environment}-${var.location}"
  location            = azurerm_resource_group.rg_signalr.location
  resource_group_name = azurerm_resource_group.rg_signalr.name

  sku {
    name     = var.signalr_config.sku_name
    capacity = var.signalr_config.capacity
  }

  # Optional
  cors {
    allowed_origins = ["http://www.example.com"]
  }

  public_network_access_enabled = false

  connectivity_logs_enabled = true
  messaging_logs_enabled    = true
  service_mode              = "Default" # Options: Serverless | Default | Classic

  # Optional
  # An upstream_endpoint block as documented below. Using this block requires the SignalR service to be Serverless. 
  #  When creating multiple blocks they will be processed in the order they are defined in.
  upstream_endpoint {
    category_pattern = ["connections", "messages"]
    event_pattern    = ["*"]
    hub_pattern      = ["hub1"]
    url_template     = "http://foo.com"
  }
}