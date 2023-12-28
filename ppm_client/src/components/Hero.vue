<script setup>
import { ref } from 'vue';
import { baseUrl } from '../constants';

const model = {
    title: ref(""),
    key: ref(""),
    generatedPwd: ref("")
}
const modalRef = ref("dialog1")
const isLoading = ref(true)
const formSubmit = async () => {
    isLoading.value = true
    if (modalRef.value) modalRef.value.showModal()
    try {
        model.generatedPwd.value = await fetchPwdFromAPI()
        isLoading.value = false
    } catch (error) {
        model.generatedPwd.value = error.message
        //console.error(error)
    }
}

const fetchPwdFromAPI = async () => {
    const requestBody = {
        title: model.title.value,
        masterKey: model.key.value
    }

    const response = await fetch(`${baseUrl}gen-pwd/`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(requestBody)
    })

    if (!response.ok) throw new Error(`HTTP Error! Status ${response.status}`)

    return response.json()
}

const copyPassword = async () => {
    await navigator.clipboard.writeText(model.generatedPwd.value)
}
</script>

<template>
    <div class="hero min-h-screen bg-base-200">
        <div class="hero-content flex-col lg:flex-row-reverse">
            <div class="text-center lg:text-left">
                <span class="flex items-center p-4">
                    <figure class="flex-auto sm:w-48 ">
                        <img src="../assets/icon_main.png"/>
                    </figure>
                    <h1 class="text-5xl font-bold flex-auto">PPassword Manager</h1>
                </span>                
                <p class="py-6 text-xl font-semibold text-slate-500">Generate passwords that take more than a trillion years to crack!</p>
            </div>
            <div class="card shrink-0 w-full max-w-sm shadow-2xl bg-base-200">
                <form class="card-body">
                    <div class="form-control">
                        <label class="label">
                            <span class="label-text">Site title/url</span>
                        </label>
                        <input v-model="model.title.value"
                        type="text"
                        placeholder="title" 
                        class="input input-bordered" 
                        required/>
                    </div>
                    <div class="form-control">
                        <label class="label">
                            <span class="label-text">Master Key</span>
                        </label>
                        <input v-model="model.key.value"
                        type="password" 
                        placeholder="key" 
                        class="input input-bordered" 
                        required/>
                        <label class="label">
                            <a href="#" class="label-text-alt link link-hover">Have an account? Log in</a>
                        </label>
                    </div>
                    <div class="form-control mt-6">
                        <button class="btn btn-primary" @click.prevent="formSubmit">Generate password</button>
                        <dialog ref="modalRef" class="modal modal-bottom sm:modal-middle" >
                            <div class="modal-box">
                                <!-- FIXME(Center the loading icon) -->
                                <span class="loading loading-infinity loading-lg" :class="isLoading?'':'invisible' "></span>
                                <div :class="isLoading?'invisible':'' " class="bg-base-content rounded-lg grid">
                                    <div class="col-start-1 row-start-1 text-slate-300 p-2 items-center">
                                        {{ model.generatedPwd.value }}
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
                                <div class="modal-action">
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
    </div>
</template>

<style scoped>

</style>