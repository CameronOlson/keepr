import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"


class KeepsService{

  async getKeeps(){
    const res = await api.get('api/keeps')
    logger.log('this is the get all keeps res', res)
    AppState.keeps = res.data
  }
  async getKeepsByProfileId(profileId){
    const res = await api.get('api/profiles/'+ profileId + '/keeps')
    logger.log('get keeps by profileId', res)
    AppState.keeps = res.data
  }
  async getKeepById(keepId){
    const res = await api.get('api/keeps/' + keepId)
    logger.log('this is the keep', res.data)
    AppState.keep = res.data

  }

  async postKeep(keepData){
    const res = await api.post('api/keeps', keepData)
    logger.log('this was the posted keep', res)
    AppState.keeps.push(res.data)
  }
  async removeKeep(keepId){
    const res = await api.delete('api/keeps/' + keepId)
    logger.log("this was deleted", res.data)
    AppState.keeps = AppState.keeps.filter(k => k.id !== keepId)
  }

  async updateViewCount(keepId, keep){
    keep.views = keep.views + 1
    const res = await api.put('api/keeps/' + keepId, keep)
    let keepIndex = AppState.keeps.findIndex(k => k.id == keep.id)
    AppState.keeps.splice(keepIndex, 1, res.data)
    logger.log("update the views", res.data)
  }

  async updateKeeps(data){
    data.keeps = data.keeps + 1
    const res = await api.put('api/keeps/' + data.id, data)
    logger.log("update the keeps", res.data)
  }
}
export const keepsService = new KeepsService()