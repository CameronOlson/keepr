import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class ProfilesService {


  async getProfileById(profileId){
    const res = await api.get('api/profiles/' + profileId)
    logger.log('this is the profile', res)
    AppState.profile = res.data
  }
}
export const profilesService = new ProfilesService()