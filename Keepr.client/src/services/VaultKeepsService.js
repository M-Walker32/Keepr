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
}
}
export const vaultKeepsService = new VaultKeepsService