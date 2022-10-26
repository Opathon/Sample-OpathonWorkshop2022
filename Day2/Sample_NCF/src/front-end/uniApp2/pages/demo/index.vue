<template>
	<view class="content">

		<view class="area">
			<view class="area-bg" :style="'background-color:'+bgStlye">
			</view>
			<view class="area-textBox">
				<view class="area-text" @tap="brighten">
					变亮
				</view>
				<view class="area-text" @tap="darken">
					变暗
				</view>
				<view class="area-text" @tap="random">
					随机
				</view>
			</view>
		</view>
	</view>
</template>

<script>
	export default {
		data() {
			return {
				title: 'Hello',
				// blue: 162, red: 75, green: 232
				bgStlye: ''
			}
		},
		onLoad() {

		},
		methods: {
			// 变亮
			brighten(){
				uni.request({
					url:'https://localhost:44311/api/Opathon.Xncf.WeixinManagerWxOpen/ColorAppService/Xncf.WeixinManagerWxOpen_ColorAppService.GetBrightenAsync',
					method:"GET",
					success: (res) => {
						console.log('变亮',res);
						const data = res.data.data
						if(data){
							const { red,blue,green } = data
							this.bgStlye = `rgb(${red},${blue},${green})`
						}else{
							uni.showToast({
								title:'获取数据失败',
								icon:"none",
								duration:1500
							})
						}
					},
					fail: (err) => {
						uni.showToast({
							title:'获取数据失败',
							icon:"none",
							duration:1500
						})
					}
				})
			},
			// 变暗
			darken(){
				uni.request({
					url:'https://localhost:44311/api/Opathon.Xncf.WeixinManagerWxOpen/ColorAppService/Xncf.WeixinManagerWxOpen_ColorAppService.GetDarkenAsync',
					method:"GET",
					success: (res) => {
						console.log('变暗',res);
						const data = res.data.data
						if(data){
							const { red,blue,green } = data
							this.bgStlye = `rgb(${red},${blue},${green})`
						}else{
							uni.showToast({
								title:'获取数据失败',
								icon:"none",
								duration:1500
							})
						}
					}
				})
			},
			// 随机 random
			random(){
				uni.request({
					url:'https://localhost:44311/api/Opathon.Xncf.WeixinManagerWxOpen/ColorAppService/Xncf.WeixinManagerWxOpen_ColorAppService.GetRandomAsync',
					method:"GET",
					success: (res) => {
						console.log('随机',res);
						const data = res.data.data
						if(data){
							const { red,blue,green } = data
							this.bgStlye = `rgb(${red},${blue},${green})`
						}else{
							uni.showToast({
								title:'获取数据失败',
								icon:"none",
								duration:1500
							})
						}
					}
				})
			}
		}
	}
</script>

<style>
	.content {
		box-sizing: border-box;
	}

	.area {
		display: flex;
		flex-direction: column;
		align-items: center;
	}

	.area-bg {
		width: 700rpx;
		height: 200rpx;
		background-color: rgb(65, 222, 152);
	}

	.area-textBox {
		width: 100%;
		display: flex;
		flex-direction: row;
		justify-content: space-evenly;
		align-items: center;
	}

	.area-text {
		margin-top: 20px;
		text-align: center;
		color: #000;
		font-size: 28rpx;
		user-select: none;
		cursor: pointer;
		padding: 10rpx 30rpx;
		border-radius: 30px;
		background-color: aquamarine;
	}
</style>
