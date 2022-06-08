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
async getById(id)
{
  const keep = await api.get('api/keeps/'+id)
  // keep.views +=1
  // logger.log(keep.data)
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
// TODO check if this works, get a toast success
async deleteKeep(id){
  await api.delete('api/keeps/'+id)
  AppState.myKeeps = AppState.myKeeps.filter(k => k.id !== id)
  return Pop.toast("Keep Deleted","success")
}
}

export const keepsService = new KeepsService