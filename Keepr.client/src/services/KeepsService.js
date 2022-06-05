import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class KeepsService{
async getKeeps(){
  const res = await api.get('api/keeps')
  logger.log(res.data)
  AppState.keeps = res.data
}
async getById(id)
{
  const keep = await api.get('api/keeps/'+id)
  logger.log(keep.data)
  AppState.activeKeep = keep.data
}
async getProfileKeeps(id){
  const res = await api.get(`api/profiles/${id}/keeps`)
  logger.log(res.data)
  AppState.myKeeps = res.data
}
async getVaultKeeps(vaultId){
  const res = await api.get(`api/vaults/${vaultId}/keeps`)
  logger.log(res.data)
  AppState.vaultKeeps = res.data
}
}

export const keepsService = new KeepsService