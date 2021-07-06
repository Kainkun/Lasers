float mean_height;

for(int i=-1; i<=1; i++)
{
    for(int j=-1; j<=1; j++)
    {
        mean_height += SAMPLE_TEXTURE2D_LOD(TrailMap, SS, float2(TexCoord.x+i*BlurSize, TexCoord.y+j*BlurSize),1).r;
    }
}
HeightPos = saturate(mean_height/9.0) * HeightMultiplier;