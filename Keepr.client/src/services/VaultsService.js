import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {

  async getVaultById(vaultId){
    const res = await api.get('api/vaults/' + vaultId)
    logger.log('[Got Vault]', res.data)
    AppState.activeVault = res.data
  }

  async getVaultKeeps(vaultId){
    const res = await api.get(`api/vaults/${vaultId}/keeps`)
    logger.log('[Got VaultKeeps]', res.data)
    AppState.vaultKeeps = res.data
  }
  async createVault(vaultData){
    const res = await api.post('api/vaults', vaultData)
    AppState.vaults.push(res.data)
  }
}

export const vaultsService = new VaultsService()