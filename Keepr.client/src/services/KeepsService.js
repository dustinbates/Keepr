import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { api } from "./AxiosService"

class KeepsService {

  async getKeeps(){
    const res = await api.get('api/keeps')
    logger.log('[GOT KEEPS]', res.data)
    AppState.keeps = res.data
  }

  async getProfileKeeps(id){
    const res = await api.get(`api/profiles/${id}/keeps`)
    logger.log('[GOT PROFILE KEEPS]', res.data)
    AppState.profileKeeps = res.data
  }

  async setActiveKeep(keep){
    const res = await api.get(`api/keeps/${keep.id}`)
    const creatorId = AppState.account.id
    if(keep.creatorId != creatorId){
      keep.views++
    }
    AppState.activeKeep = keep
  }

  async createKeep(keepData, userId){
    const res = await api.post('api/keeps', keepData)
    Pop.toast("Created Keep!", 'success')
    AppState.keeps.push(res.data)
    if(res.data.creatorId == userId){
      AppState.profileKeeps.push(res.data)
    }
    
  }

  async deleteKeep(keepId){
    const res = await api.delete(`api/keeps/${keepId}`)
    Pop.toast("Deleted Keep!", 'success')
    let deleteIndex = AppState.keeps.findIndex(k => k.id == keepId)
    let profileIndex = AppState.profileKeeps.findIndex(pk => pk.id == keepId)
    AppState.keeps.splice(deleteIndex, 1)
    AppState.profileKeeps.splice(profileIndex, 1)
  }


}

export const keepsService = new KeepsService()