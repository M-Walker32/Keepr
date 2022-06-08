import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultKeepsService{
async addToVault(vaultId, keepId){
  const vaultKeep = {
    vaultId: vaultId,
    keepId: keepId
  }
  logger.log(vaultKeep)
  const newVaultKeep = await api.post('api/vaultkeeps', vaultKeep)
  logger.log(newVaultKeep.data)
  const keep = await api.get('api/keeps/'+ keepId)
  // keep.data.views -=1
  AppState.activeKeep = keep.data
  AppState.myVaultKeeps.push(keep.data)
  logger.log(keep.data)
}
}
export const vaultKeepsService = new VaultKeepsService