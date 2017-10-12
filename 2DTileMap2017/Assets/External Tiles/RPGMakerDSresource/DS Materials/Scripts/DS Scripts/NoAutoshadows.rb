#==============================================================================
# No AutoShadows
#   by NeonBlack
# -- Level: Easy, Normal
# -- Requires: n/a
# -- This simply removes autoshadows.
#==============================================================================

class Spriteset_Map
  def create_tilemap
    @tilemap = Tilemap.new(@viewport1)
    @tilemap.map_data = $game_map.data.dup
    $game_map.width.times do |i1|
      $game_map.height.times do |i2|
        @tilemap.map_data[i1, i2, 3] = 0
      end
    end
    load_tileset
  end

  def update_tilemap
    @tilemap.map_data = $game_map.data.dup
    $game_map.width.times do |i1|
      $game_map.height.times do |i2|
        @tilemap.map_data[i1, i2, 3] = 0
      end
    end
    @tilemap.ox = $game_map.display_x * 32
    @tilemap.oy = $game_map.display_y * 32
    @tilemap.update
  end
end