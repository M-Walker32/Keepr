import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class ProfilesService{
async getById(id){
 const profile = await api.get('api/profiles/'+id)
//  logger.log(profile.data)
 AppState.activeProfile = profile.data
}
}
export const profilesService = new ProfilesService