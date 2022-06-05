import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultsService{
  async getById(id){
    const vault = await api.get('api/vaults/'+id)
    logger.log(vault.data)
    AppState.activeVault = vault.data
  }
  async getProfileVaults(id){
    const res = await api.get(`api/profiles/${id}/vaults`)
    logger.log(res.data)
    AppState.myVaults = res.data
  }
}
export const vaultsService = new VaultsService