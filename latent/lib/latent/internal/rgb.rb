class Rgb

  require File.join 'paint', 'paint'
  require File.join 'paint', 'colour'

  def convert contents
    components = contents.split ','
    volume = components.at 0

    if volume.nil?
        return Paint.new 0, nil
    end

    colour_parts = components.slice 1, 4

    if colour_parts.length != 3
      return Paint.new Integer(volume), nil
    end

    colour = interpret colour_parts

    Paint.new Integer(volume), colour
  end

  def interpret channels
    red = reject_invalid_rgb!(channels.at 0)
    green = reject_invalid_rgb!(channels.at 1)
    blue = reject_invalid_rgb!(channels.at 2)

    Colour.new red, green, blue
  end

  def reject_invalid_rgb! channel
    if not within_rgb_channels channel
      raise 'the channel value is not valid: <'+channel+'>'
    end

    Integer(channel)
  end

  def within_rgb_channels channel
    as_int = Integer(channel)

    as_int >= 0 and as_int <= 255
  end

end
