<script setup>
import { auth_getPasswordFromAPI, deleteSavedSiteFromApi, getSavedSitesFromApi } from '../apiCalls';
import Site from './Site.vue';
import { onMounted, ref } from 'vue';

const siteList = ref([])
const title = ref("")
const modalRef = ref("dialogf")
const passwordGenerated = ref("")
const isLoading = ref(false)

onMounted(async () => {
    siteList.value = await getSavedSitesFromApi()
    console.log(siteList.value)
})

const onDelete = async (site) => {
    try {
        await deleteSavedSiteFromApi({ title: site })
        siteList.value = await getSavedSitesFromApi()
    } catch (error) {
        console.error(error)
    }
}

const formSubmit = async () => {
    isLoading.value = true
    const requestBody = {
        title: title.value
    }
    modalRef.value.showModal()
    try {
        passwordGenerated.value = await auth_getPasswordFromAPI(requestBody)
        isLoading.value = false
    } catch (error) {
        passwordGenerated.value = 'Error fetching rss'
        console.error(error)
        isLoading.value = false
    }

    siteList.value = await getSavedSitesFromApi()
}
</script>

<template>
    <div class="hero min-h-screen bg-base-200">
        
        <div class="hero-content flex-col items-center">
            
            <h2 class="text-4xl font-bold flex-auto">History</h2>

            <div class="card w-96 bg-base-100 shadow-xl">
                <div class="card-body items-center text-center">
                    <div class="card-title">Create a new password</div>
                    <form @submit.prevent="formSubmit">
                        <input v-model="title"
                        type="text" 
                        placeholder="Title/Url" 
                        class="input input-bordered" 
                         reuired />
                        <div class="form-control mt-6">
                            <button class="btn btn-primary" type="submit">Generate</button>
                            <dialog ref="modalRef" class="modal modal-bottom sm:modal-middle">
                                <div class="modal-box relative h-40">
                                    <span class="absolute w-50 top-1/2 left-1/2 translate-y-[-50%] translate-x-[-50%] loading loading-infinity loading-lg" :class="isLoading?'':'hidden' "></span>
                                    <div :class="isLoading?'hidden':'' " class="bg-base-content rounded-lg grid">
                                        <div class="col-start-1 row-start-1 text-slate-300 p-2 items-center">
                                            {{ passwordGenerated }}
                                        </div>
                                        <div class="col-start-1 row-start-1 flex items-start justify-end p-2 rtl:justify-start">
                                            <div class="tooltip tooltip-left tooltip-accent" data-tip="copy">
                                                <button class="btn btn-square btn-sm btn-neutral" @click.prevent="copyPassword">
                                                    <svg class="h-5 w-5 fill-current" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 32 32">
                                                        <path d="M 16 3 C 14.742188 3 13.847656 3.890625 13.40625 5 L 6 5 L 6 28 L 26 28 L 26 5 L 18.59375 5 C 18.152344 3.890625 17.257813 3 16 3 Z M 16 5 C 16.554688 5 17 5.445313 17 6 L 17 7 L 20 7 L 20 9 L 12 9 L 12 7 L 15 7 L 15 6 C 15 5.445313 15.445313 5 16 5 Z M 8 7 L 10 7 L 10 11 L 22 11 L 22 7 L 24 7 L 24 26 L 8 26 Z"></path>
                                                    </svg>
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-action" :class="isLoading?'hidden':'' ">
                                        <form method="dialog">
                                            <button class="btn">Close</button>
                                        </form>
                                    </div>
                                </div>
                            </dialog>
                        </div>
                    </form>
                </div>
            </div>
            
            <div v-for="site in siteList" class="flex flex-row items-center">
                <Site :title="site" @deleted="onDelete(site)"/>
            </div>
        </div>
    </div>
</template>

<style scoped></style>