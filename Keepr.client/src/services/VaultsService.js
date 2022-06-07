import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultsService{
  async getById(id){
    const vault = await api.get('api/vaults/'+id)
    // logger.log(vault.data)
    AppState.activeVault = vault.data
  }
  async getProfileVaults(id){
    const res = await api.get(`api/profiles/${id}/vaults`)
    // logger.log(res.data)
    AppState.myVaults = res.data
  }
  async getVaultKeeps(id){
    const res = await api.get(`api/vaults/${id}/keeps`)
    // logger.log('vaultkeeps:',res.data)
    AppState.vaultKeeps = res.data
  }
  async createVault(vaultdata){
    logger.log(vaultdata)
    const newVault = await api.post('api/vaults', vaultdata)
    logger.log(newVault.data)
    AppState.myVaults.push(newVault.data)
  }
}
export const vaultsService = new VaultsService