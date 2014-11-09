class Cmyk

  def convert contents
    components = contents.split ','
    volume = components.at 0

    if volume.nil?
      return Paint.new 0, nil
    end

    colour_parts = components.slice 1, 5

    if colour_parts.length != 4
      return Paint.new Integer(volume), nil
    end

    colour = interpret colour_parts

    return Paint.new Integer(volume), colour
  end

  def interpret colour
    cyan    = to_fraction Integer(colour.at 0)
    magenta = to_fraction Integer(colour.at 1)
    yellow  = to_fraction Integer(colour.at 2)
    key     = to_fraction Integer(colour.at 3)

    convert_to_canonical cyan, magenta, yellow, key
  end

  def convert_to_canonical c, m, y, k
    from = from_rgb k
    Colour.new (from.call c), (from.call m), (from.call y)
  end

  def from_rgb k
    lambda do | v |
      1 - [1, v  * (1-k) + k].min
    end
  end

  def to_fraction v
    v / 100.0
  end

end
