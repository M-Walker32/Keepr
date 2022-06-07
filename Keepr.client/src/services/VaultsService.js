import { AppState } from "../AppState.js"
import { router } from "../router.js"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { api } from "./AxiosService.js"

class VaultsService{
  async getById(id){
    const userId = AppState.account.id
    const vault = await api.get('api/vaults/'+id)
    if(vault.isPrivate && vault.creatorId == userId){
      AppState.activeVault = vault.data
    }
    if(!vault.isPrivate){
      AppState.activeVault = vault.data
    }
    else{
      return Error
    }
  }
  async getProfileVaults(id){
    const res = await api.get(`api/profiles/${id}/vaults`)
    logger.log('my vaults ',res.data)
    AppState.myVaults = res.data
  }
  async getVaultKeeps(id){
    const res = await api.get(`api/vaults/${id}/keeps`)
    // logger.log('vaultkeeps:',res.data)
    AppState.vaultKeeps = res.data
  }
  async createVault(vaultdata){
    // logger.log(vaultdata)
    const newVault = await api.post('api/vaults', vaultdata)
    // logger.log(newVault.data)
    AppState.myVaults.push(newVault.data)
  }
  async deleteVault(vaultId){
    await api.delete('api/vaults/'+ vaultId)
  }
}
export const vaultsService = new VaultsService