import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { api } from "./AxiosService.js"

class KeepsService{
async getKeeps(){
  const res = await api.get('api/keeps')
  // logger.log(res.data)
  AppState.keeps = res.data
}
async getById(keepId)
{
    const keep = await api.get('api/keeps/'+ keepId)
    logger.log(keep.data)
    keep.data.views +=1
    AppState.activeKeep = keep.data  
}
async getProfileKeeps(id){
  const res = await api.get(`api/profiles/${id}/keeps`)
  // logger.log(res.data)
  AppState.myKeeps = res.data
}
async getVaultKeeps(vaultId){
  const res = await api.get(`api/vaults/${vaultId}/keeps`)
  logger.log('vaultkeeps',res.data)
  AppState.vaultKeeps = res.data
}
async createKeep(newKeep){
  const keep = await api.post('api/keeps', newKeep)
  // logger.log(keep.data)
  AppState.myKeeps.unshift(keep.data)
}
async deleteKeep(id){
  await api.delete('api/keeps/'+id)
  AppState.myKeeps = AppState.myKeeps.filter(k => k.id !== id)
  AppState.keeps = AppState.keeps.filter(k => k.id !== id)
  return Pop.toast("Keep Deleted","success")
}
async unSaveKeep(vaultKeepId, vaultId){
  const keep = await api.delete('api/vaultKeeps/'+vaultKeepId)
  AppState.vaultKeeps = this.getVaultKeeps(vaultId)
  return Pop.toast("Keep Removed from Vault","success")
}
}

export const keepsService = new KeepsService